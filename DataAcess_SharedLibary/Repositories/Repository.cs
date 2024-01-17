

using Dapper;
using DataAcess_SharedLibary.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace DataAcess_SharedLibary.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    private readonly string _connectionString;

    protected Repository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public virtual bool Execute(string query)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute(query);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

    public virtual bool Execute(string query, TEntity entity)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute(query, entity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

    public virtual IDataReader Read(string query)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            var result = conn.ExecuteReader(query);

            if (result != null)
                return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public virtual IDataReader Reader(string query, TEntity entity)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            var result = conn.ExecuteReader(query, entity);

            if (result != null)
                return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    public virtual TEntity GetOne(string query, TEntity entity)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            var result = conn.QueryFirstOrDefault<TEntity>(query, entity);

            if (result != null)
                return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    

 


    public IEnumerable<User> Select(string query, string email)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<User>(query, new { Email = email });
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public bool Delete(string query, string email)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            int rowsAffected = conn.Execute(query, new { Email = email });

            if (rowsAffected > 0)
            {
                return true;
            }
            else
                return false;
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IEnumerable<User> GetAllFromUserModel(string query)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<User>(query);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }



    public bool Update(string query, string email)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            int rowsAffected = conn.Execute(query, new { Email = email });

            if (rowsAffected > 0)
            {
                return true;
            }
            else
                return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    public virtual IEnumerable<string> ReadCheck(string query, string email, User user)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);

            return conn.Query<string>(query, new { Email = email }) ?? Enumerable.Empty<string>();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return Enumerable.Empty<string>();

    }

}
