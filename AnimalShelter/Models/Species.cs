using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace AnimalShelter.Models
{
  public class Species
  {
    private string _speciesName;
    private int _id;

    public Species(string speciesName, int id = 0)
    {
      _speciesName = speciesName;
      _id = id;
    }

    public override bool Equals(System.Object otherSpecies)
    {
      if (!(otherSpecies is Species))
      {
        return false;
      }
      else
      {
        Species newSpecies = (Species) otherSpecies;
        bool idEquality = this.GetId() == newSpecies.GetId();
        bool nameEquality = this.GetAnimalSpecies() == newSpecies.GetAnimalSpecies();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetAnimalSpecies()
    {
      return _speciesName;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO species (name) VALUES (@speciesname);";

      MySqlParameter speciesName = new MySqlParameter();
      speciesName.ParameterName = "@speciesname";
      speciesName.Value = this._speciesName;
      cmd.Parameters.Add(speciesName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
    }

    public static List<Species> GetAll()
    {
      List<Species> allSpeciess = new List<Species> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM species;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int SpeciesId = rdr.GetInt32(0);
        string SpeciesName = rdr.GetString(1);
        Species newSpecies = new Species(SpeciesName, SpeciesId);
        allSpeciess.Add(newSpecies);
      }
      return allSpeciess;
    }

    public static List<Species> GetAllAlphabetical()
    {
      List<Species> allSpeciess = new List<Species> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM species ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int SpeciesId = rdr.GetInt32(0);
        string SpeciesName = rdr.GetString(1);
        Species newSpecies = new Species(SpeciesName, SpeciesId);
        allSpeciess.Add(newSpecies);
      }
      return allSpeciess;
    }

    public static Species Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM species WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int SpeciesId = 0;
      string SpeciesName = "";

      while(rdr.Read())
      {
        SpeciesId = rdr.GetInt32(0);
        SpeciesName = rdr.GetString(1);
      }
      Species newSpecies = new Species(SpeciesName, SpeciesId);
      return newSpecies;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM species;";
      cmd.ExecuteNonQuery();
    }

    public List<Animal> GetAnimals()
    {
     List<Animal> allCategoryAnimals = new List<Animal> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM animals WHERE species_id = @species_id ORDER BY admittance;";

     MySqlParameter speciesId = new MySqlParameter();
     speciesId.ParameterName = "@species_id";
     speciesId.Value = this._id;
     cmd.Parameters.Add(speciesId);


     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int animalId = rdr.GetInt32(0);
       string animalName = rdr.GetString(1);
       int animalCategoryId = rdr.GetInt32(2);
       string gender = rdr.GetString(3);
       DateTime admittance = Convert.ToDateTime(rdr["admittance"]);
       string breed = rdr.GetString(5);

       Animal newAnimal = new Animal(animalName, animalCategoryId, gender, admittance, breed, animalId);
       allCategoryAnimals.Add(newAnimal);
     }
     return allCategoryAnimals;
    }

    public List<Animal> GetAnimalsBreed()
    {
     List<Animal> allCategoryAnimals = new List<Animal> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM animals WHERE species_id = @species_id ORDER BY breed;";

     MySqlParameter speciesId = new MySqlParameter();
     speciesId.ParameterName = "@species_id";
     speciesId.Value = this._id;
     cmd.Parameters.Add(speciesId);


     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int animalId = rdr.GetInt32(0);
       string animalName = rdr.GetString(1);
       int animalCategoryId = rdr.GetInt32(2);
       string gender = rdr.GetString(3);
       DateTime admittance = Convert.ToDateTime(rdr["admittance"]);
       string breed = rdr.GetString(5);

       Animal newAnimal = new Animal(animalName, animalCategoryId, gender, admittance, breed, animalId);
       allCategoryAnimals.Add(newAnimal);
     }
     return allCategoryAnimals;
    }
  }
}
