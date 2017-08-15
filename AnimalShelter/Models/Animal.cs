using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace AnimalShelter.Models
{
  public class Animal
  {
    private int _id;
    private string _animalName;
    private int _speciesId;
    private string _gender;
    private DateTime _admittanceDate;
    private string _breed;

    public Animal(string animalName, int speciesId, string gender, DateTime admittance, string breed, int id=0)
    {
      _id = id;
      _animalName = animalName;
      _speciesId = speciesId;
      _gender = gender;
      _admittanceDate = admittance;
      _breed = breed;
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = this.GetId() == newAnimal.GetId();
        bool nameEquality = this.GetName() == newAnimal.GetName();
        bool speciesEquality = this.GetSpeciesId() == newAnimal.GetSpeciesId();
        bool GenderEquality = this.GetGender() == newAnimal.GetGender();
        bool AdmittanceEquality = this.GetAdmittance() == newAnimal.GetAdmittance();
        bool BreedEquality = this.GetBreed() == newAnimal.GetBreed();
        return (nameEquality && idEquality && speciesEquality && GenderEquality && AdmittanceEquality && BreedEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _animalName;
    }

    public string GetDetails()
    {
      return "ID: " +_id + ", Name: " + _animalName + ", SpeciesID: " + _speciesId + ", Gender: " + _gender + ", Date" + _admittanceDate + ", Breed: " + _breed;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetSpeciesId()
    {
      return _speciesId;
    }

    public string GetGender()
    {
      return _gender;
    }

    public DateTime GetAdmittance()
    {
      return _admittanceDate;
    }

    public string GetBreed()
    {
      return _breed;
    }

    public void Save()
    {
         MySqlConnection conn = DB.Connection();
         conn.Open();

         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO animals (name, species_id, gender, admittance, breed) VALUES (@animalname, @species_id, @gender, @admittance, @breed);";

         MySqlParameter animalname = new MySqlParameter();
         animalname.ParameterName = "@animalname";
         animalname.Value = this._animalName;
         cmd.Parameters.Add(animalname);

         MySqlParameter speciesId = new MySqlParameter();
         speciesId.ParameterName = "@species_id";
         speciesId.Value = this._speciesId;
         cmd.Parameters.Add(speciesId);

         MySqlParameter gender = new MySqlParameter();
         gender.ParameterName = "@gender";
         gender.Value = this._gender;
         cmd.Parameters.Add(gender);

         MySqlParameter admittance = new MySqlParameter();
         admittance.ParameterName = "@admittance";
         admittance.Value = this._admittanceDate;
         cmd.Parameters.Add(admittance);

         MySqlParameter breed = new MySqlParameter();
         breed.ParameterName = "@breed";
         breed.Value = this._breed;
         cmd.Parameters.Add(breed);

         cmd.ExecuteNonQuery();
         _id = (int) cmd.LastInsertedId;
    }

    public static List<Animal> GetAll()
    {
        List<Animal> allAnimals = new List<Animal> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM animals;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int Id = rdr.GetInt32(0);
          string animalName = rdr.GetString(1);
          int speciesId = rdr.GetInt32(2);
          string gender = rdr.GetString(3);
          DateTime admittance = Convert.ToDateTime(rdr["admittance"]);
          string breed = rdr.GetString(5);


          Animal newAnimal = new Animal(animalName, speciesId, gender, admittance, breed, Id);
          allAnimals.Add(newAnimal);
        }
        return allAnimals;
    }

    public static List<Animal> GetAllByBreed()
    {
        List<Animal> allAnimals = new List<Animal> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM animals ORDER BY breed;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int Id = rdr.GetInt32(0);
          string animalName = rdr.GetString(1);
          int speciesId = rdr.GetInt32(2);
          string gender = rdr.GetString(3);
          DateTime admittance = Convert.ToDateTime(rdr["admittance"]);
          string breed = rdr.GetString(5);


          Animal newAnimal = new Animal(animalName, speciesId, gender, admittance, breed, Id);
          allAnimals.Add(newAnimal);
        }
        return allAnimals;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animals;";
      cmd.ExecuteNonQuery();
    }

    public static void DeleteSpeciesAnimals(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animals WHERE species_id = @specieId;";

      MySqlParameter specieId = new MySqlParameter();
      specieId.ParameterName = "@specieId";
      specieId.Value = id;
      cmd.Parameters.Add(specieId);

      cmd.ExecuteNonQuery();
    }


    public static Animal Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM animals WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int animalId = 0;
        string animalName = "";
        int speciesId = 12;
        string gender = "";
        DateTime admittance = new DateTime();
        string breed = "";

        while(rdr.Read())
        {
          animalId = rdr.GetInt32(0);
          animalName = rdr.GetString(1);
          speciesId = rdr.GetInt32(2);
          gender = rdr.GetString(3);
          admittance = Convert.ToDateTime(rdr["admittance"]);
          breed = rdr.GetString(5);
        }
        Animal newAnimal = new Animal(animalName, speciesId, gender, admittance, breed, animalId);
        return newAnimal;
    }
  }

}
