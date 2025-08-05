using MediCareCMSWebApi.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MediCareCMSWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        #region 1- Get All Users with Password
        public List<UserInputModel> GetAllUsers()
        {
            List<UserInputModel> users = new List<UserInputModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllUsersWithPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new UserInputModel
                        {
                            UserId = reader["UserId"] != DBNull.Value ? (int)reader["UserId"] : 0,
                            Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : null!,
                            Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null!,
                            RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : null!,
                            FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                            LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                            Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null,
                            Addresss = reader["Addresss"] != DBNull.Value ? reader["Addresss"].ToString() : null,
                            Dob = reader["Dob"] != DBNull.Value ? (DateTime?)reader["Dob"] : null,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : null,
                            Contact = reader["Contact"] != DBNull.Value ? reader["Contact"].ToString() : null,
                            RoleId = reader["RoleId"] != DBNull.Value ? (int)reader["RoleId"] : 0,
                            DepartmentId = reader["DepartmentId"] != DBNull.Value ? (int?)reader["DepartmentId"] : null,
                            IsActive = reader["IsActive"] != DBNull.Value ? (bool)reader["IsActive"] : false
                        });
                    }

                    reader.Close();
                }
            }

            return users;
        }
        #endregion



        #region 2- Insert User with Auto Credentials
        public (string Username, string Password) AddUser(UserInputModel user)
        {
            string username = null;
            string password = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_InsertStaffWithAutoCredentials", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Required Parameters
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

                // Optional Parameters (nullable)
                cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Addresss", user.Addresss ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Dob", user.Dob ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BloodGroup", user.BloodGroup ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Contact", user.Contact ?? (object)DBNull.Value);

                // DepartmentId only for doctors; otherwise DBNull
                if (user.RoleName == "Doctor")
                    cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId ?? (object)DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@DepartmentId", DBNull.Value);

                // IsActive default true
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive ?? true);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        username = reader["Username"].ToString();
                        password = reader["Password"].ToString();
                    }
                }
            }

            return (username, password);
        }
        #endregion

        public bool UpdateUser(int id, UserInputModel user)
        {
            if (!UserExists(id))
                return false;

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_UpdateStaff", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", id);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Addresss", user.Addresss ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Dob", user.Dob ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BloodGroup", user.BloodGroup ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", user.Contact ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
            cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        private bool UserExists(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserId = @UserId", con);
            cmd.Parameters.AddWithValue("@UserId", id);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }


        public bool DeactivateUser(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DeactivateStaff", con);  // You must create this SP

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", id);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;

        }
        public UserInputModel? GetUserById(int id)
        {
            UserInputModel? user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetUserById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserInputModel
                        {
                            UserId = reader["UserId"] != DBNull.Value ? (int)reader["UserId"] : 0,
                            Username = reader["Username"]?.ToString() ?? string.Empty,
                            Password = reader["Password"]?.ToString() ?? string.Empty,
                            RoleName = reader["RoleName"]?.ToString() ?? string.Empty,
                            FirstName = reader["FirstName"]?.ToString(),
                            LastName = reader["LastName"]?.ToString(),
                            Gender = reader["Gender"]?.ToString(),
                            Addresss = reader["Addresss"]?.ToString(),
                            Dob = reader["Dob"] != DBNull.Value ? (DateTime?)reader["Dob"] : null,
                            Email = reader["Email"]?.ToString(),
                            BloodGroup = reader["BloodGroup"]?.ToString(),
                            Contact = reader["Contact"]?.ToString(),
                            RoleId = reader["RoleId"] != DBNull.Value ? (int)reader["RoleId"] : 0,
                            DepartmentId = reader["DepartmentId"] != DBNull.Value ? (int?)reader["DepartmentId"] : null,
                            IsActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"]
                        };
                    }
                }
            }

            return user;
        }



    }
}
