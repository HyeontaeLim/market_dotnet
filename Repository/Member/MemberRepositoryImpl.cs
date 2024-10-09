using market.DbConnection;
using market.Domain;
using MySql.Data.MySqlClient;

namespace market.Repository;

public class MemberRepositoryImpl : IMemberRepository
{
    
    private readonly IDbConnectionFactory _connectionFactory;

    public MemberRepositoryImpl(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public Member SaveMember(Member member)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO member (username, password, email, name, gender, address) values " +
                    "(@username, @password, @email, @name, @gender, @address); SELECT LAST_INSERT_ID();";
                command.Parameters.Add(new MySqlParameter("@username", member.Username));
                command.Parameters.Add(new MySqlParameter("@password", member.Password));
                command.Parameters.Add(new MySqlParameter("@email", member.Email));
                command.Parameters.Add(new MySqlParameter("@name", member.Name));
                command.Parameters.Add(new MySqlParameter("@gender", member.Gender.ToString()));
                command.Parameters.Add(new MySqlParameter("@address", member.Address));
                
                long memberId = Convert.ToInt64(command.ExecuteScalar());
                member.MemberId = memberId;
            }
        }
        return member;
    }

    public IEnumerable<Member> FindMembersAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT member_id, username, password, email, name, gender, address FROM member";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Member(
                        reader.GetInt64(0), 
                        reader.GetString(1), 
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        Enum.Parse<Gender>(reader.GetString(5)),
                        reader.GetString(6)
                    );
                }
            }
        }

    }

    public Member? FindByUsername(string username)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT member_id, username, password, email, name, gender, address FROM member " +
                                  "WHERE username = @username";
            command.Parameters.Add(new MySqlParameter("@username", username));
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Member(
                        reader.GetInt64(0), 
                        reader.GetString(1), 
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        Enum.Parse<Gender>(reader.GetString(5)),
                        reader.GetString(6)
                    );
                }

                return null;
            }
        }
    }

    public Member? FindById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT member_id, username, password, email, name, gender, address FROM member " +
                                  "WHERE member_id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Member(
                        reader.GetInt64(0), 
                        reader.GetString(1), 
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        Enum.Parse<Gender>(reader.GetString(5)),
                        reader.GetString(6)
                    );
                }

                return null;
            }
        }

    }

    public void DeleteById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from member where member_id = @id";
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }
    }

    public Member UpdateById(Member member, long id)
    {
        throw new NotImplementedException();
    }
}