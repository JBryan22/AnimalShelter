using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using AnimalShelter.Models;
//
namespace AnimalShelter.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/species/add")]
    public ActionResult SpeciesForm()
    {
      return View();
    }

    [HttpPost("/species")]
    public ActionResult Specie()
    {
      Species newSpecies = new Species(Request.Form["name"]);
      newSpecies.Save();
      return View(Species.GetAll());
    }

    [HttpGet("/species/az")]
    public ActionResult SpeciesAlpha()
    {
      return View("Specie", Species.GetAllAlphabetical());
    }

    [HttpGet("/species")]
    public ActionResult Speci()
    {
      return View("Specie", Species.GetAll());
    }

    [HttpGet("/species/{id}")]
    public ActionResult SpeciesDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Species selectedSpecies = Species.Find(id);
      List<Animal> theseAnimals = selectedSpecies.GetAnimals();
      model.Add("species", selectedSpecies);
      model.Add("animals", theseAnimals);
      return View(model);
    }

    [HttpGet("/species/{id}/animal/new")]
    public ActionResult AnimalForm(int id)
    {
      return View(Species.Find(id));
    }

    [HttpPost("/species/{id}")]
    public ActionResult SpeciesDetailNew(int id)
    {
      Animal newAnimal = new Animal(Request.Form["name"], id, Request.Form["gender"], DateTime.Now, Request.Form["breed"]);
      newAnimal.Save();
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Species selectedSpecies = Species.Find(id);
      List<Animal> theseAnimals = selectedSpecies.GetAnimals();
      model.Add("species", selectedSpecies);
      model.Add("animals", theseAnimals);
      return View("SpeciesDetail", model);
    }

    [HttpGet("/species/{id}/animal/{id2}")]
    public ActionResult AnimalDetail(int id, int id2)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Species selectedSpecies = Species.Find(id);
      Animal thisAnimal = Animal.Find(id2);
      model.Add("species", selectedSpecies);
      model.Add("animals", thisAnimal);
      return View(model);
    }

    [HttpGet("/species/{id}/animal/breed")]
    public ActionResult AnimalsBreedOrdered(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Species selectedSpecies = Species.Find(id);
      List<Animal> theseAnimals = selectedSpecies.GetAnimalsBreed();
      model.Add("species", selectedSpecies);
      model.Add("animals", theseAnimals);

      return View("SpeciesDetail", model);
    }

    [HttpGet("/species/delete")]
    public ActionResult SpeciesDelete()
    {
      Species.DeleteAll();
      Animal.DeleteAll();
      return View("Specie", Species.GetAll());
    }

    [HttpGet("/species/{id}/animals/adopt")]
    public ActionResult AnimalAdopt(int id)
    {
      Animal.DeleteSpeciesAnimals(id);
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Species selectedSpecies = Species.Find(id);
      List<Animal> theseAnimals = selectedSpecies.GetAnimals();
      model.Add("species", selectedSpecies);
      model.Add("animals", theseAnimals);
      return View("SpeciesDetail", model);
    }

  }
}
