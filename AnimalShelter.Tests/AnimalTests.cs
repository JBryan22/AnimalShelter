using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using AnimalShelter.Models;

namespace AnimalShelter.Tests
{
  [TestClass]
  public class AnimalTests : IDisposable
  {

    public void Dispose()
    {
      Animal.DeleteAll();
    }

    public AnimalTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=animalshelter_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Animal.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAnimalsAreTheSame_Animal()
    {
      Animal firstAnimal = new Animal("Dog", 1, "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      Animal secondAnimal = new Animal("Dog", 1, "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");

      Assert.AreEqual(firstAnimal, secondAnimal);
    }

    [TestMethod]
    public void Save_SavesToDatabase_AnimalList()
    {
      //Arrange
      Animal testAnimal = new Animal("Dog", 1, "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");

      //Act
      testAnimal.Save();
      List<Animal> result = Animal.GetAll();
      List<Animal> testList = new List<Animal>{testAnimal};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      Animal testAnimal = new Animal("Dog", 1, "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      testAnimal.Save();

      //Act
      Animal savedAnimal = Animal.GetAll()[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsAnimalInDatabase_Animal()
    {
      //Arrange
      Animal testAnimal = new Animal("Dog", 1, "male", Convert.ToDateTime("1999-09-15 09:35:07"), "German Sheperd");
      testAnimal.Save();

      //Act
      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      //Assert
      Assert.AreEqual(testAnimal, foundAnimal);
    }

  }

}
