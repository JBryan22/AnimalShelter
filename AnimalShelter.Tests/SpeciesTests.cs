using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using AnimalShelter.Models;

namespace AnimalShelter.Tests
{
  [TestClass]
  public class SpeciesTest : IDisposable
  {

    public SpeciesTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=animalshelter_test;";
    }

    [TestMethod]
    public void GetAll_SpeciesEmptyAtFirst_0()
    {
      int result = Species.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Species()
    {
      //Arrange, Act
      Species firstSpecies = new Species("Bun");
      Species secondSpecies = new Species("Bun");

      //Assert
      Assert.AreEqual(firstSpecies, secondSpecies);
    }

    [TestMethod]
    public void Save_SavesSpeciesToDatabase_SpeciesList()
    {
      //Arrange
      Species testSpecies = new Species("Bun");
      testSpecies.Save();

      //Act
      List<Species> result = Species.GetAll();
      List<Species> testList = new List<Species>{testSpecies};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


  [TestMethod]
    public void Save_DatabaseAssignsIdToSpecies_Id()
    {
      //Arrange
      Species testSpecies = new Species("Bun");
      testSpecies.Save();

      //Act
      Species savedSpecies = Species.GetAll()[0];

      int result = savedSpecies.GetId();
      int testId = testSpecies.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }


    [TestMethod]
    public void Find_FindsSpeciesInDatabase_Species()
    {
      //Arrange
      Species testSpecies = new Species("Bun");
      testSpecies.Save();

      //Act
      Species foundSpecies = Species.Find(testSpecies.GetId());

      //Assert
      Assert.AreEqual(testSpecies, foundSpecies);
    }

    public void Dispose()
    {
      Animal.DeleteAll();
      Species.DeleteAll();
    }

    [TestMethod]
    public void GetAnimals_RetrievesAllAnimalsWithSpecies_AnimalList()
    {
      Species testSpecies = new Species("Bun");
      testSpecies.Save();

      Animal firstAnimal = new Animal("Dog", testSpecies.GetId(), "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      firstAnimal.Save();
      Animal secondAnimal = new Animal("Dog", testSpecies.GetId(), "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      secondAnimal.Save();


      List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
      List<Animal> resultAnimalList = testSpecies.GetAnimals();
      Console.WriteLine(testAnimalList[0].GetDetails());
      Console.WriteLine(resultAnimalList[0].GetDetails());

      CollectionAssert.AreEqual(testAnimalList, resultAnimalList);
    }
    // [TestMethod]
    // public void GetAnimals_RetrievesAllAnimalsWithSpeciesInAlphabeticalOrderByName_AnimalList()
    // {
    //   Species testSpecies = new Species("Bun");
    //   testSpecies.Save();
    //
    //   Animal firstAnimal = new Animal("Bowser", testSpecies.GetId(), "male", Convert.ToDateTime("2007-09-15 09:35:07"), "German Sheperd");
    //   firstAnimal.Save();
    //
    //   Animal secondAnimal = new Animal("Ziggy", testSpecies.GetId(), "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
    //   secondAnimal.Save();
    //
    //   Animal thirdAnimal = new Animal("Fido", testSpecies.GetId(), "male", Convert.ToDateTime("2015-09-15 09:35:07"), "German Sheperd");
    //   thirdAnimal.Save();
    //
    //   List<Animal> testAnimalList = new List<Animal> {firstAnimal, thirdAnimal, secondAnimal};
    //   List<Animal> resultAnimalList = testSpecies.GetAnimals();
    //
    //   CollectionAssert.AreEqual(testAnimalList, resultAnimalList);
    // }

    [TestMethod]
    public void GetAnimals_RetrievesAllAnimalsWithSpeciesInOrderByAdmittance_AnimalList()
    {
      Species testSpecies = new Species("Bun");
      testSpecies.Save();

      Animal firstAnimal = new Animal("Bowser", testSpecies.GetId(), "male", Convert.ToDateTime("2007-09-15 09:35:07"), "German Sheperd");
      firstAnimal.Save();

      Animal secondAnimal = new Animal("Ziggy", testSpecies.GetId(), "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      secondAnimal.Save();

      Animal thirdAnimal = new Animal("Fido", testSpecies.GetId(), "male", Convert.ToDateTime("2015-09-15 09:35:07"), "German Sheperd");
      thirdAnimal.Save();

      List<Animal> testAnimalList = new List<Animal> {secondAnimal, firstAnimal, thirdAnimal};
      List<Animal> resultAnimalList = testSpecies.GetAnimals();

      CollectionAssert.AreEqual(testAnimalList, resultAnimalList);
    }
  }
}
