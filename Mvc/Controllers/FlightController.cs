using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.ApiCall;
using ARQ.Maqueta.Presentation.Mvc.ViewModels;
using ARQ.Maqueta.Entities.Entities;
using ARQ.Maqueta.Presentation.Mvc.Models.Shared;
using System.Threading.Tasks;
using ARQ.Maqueta.Entities;
using AutoMapper;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers;

namespace ARQ.Maqueta.Presentation.Mvc.Controllers
{
    public class FlightController : Controller
    {
        #region Members

        private FlightApiService flightService;
        private IEnumerable<AiportViewModel> aiportList;

        #endregion

        #region Constructors

        public FlightController()
        {
            this.flightService = new FlightApiService();
            this.aiportList = FileHelper.GetAiportsListItemsFromJsonFile();
        }

        #endregion

        // GET: Flight
        public ActionResult Index()
        {
            IndexFlightViewModel viewModel = new IndexFlightViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Filter(IndexFlightViewModel model, string returnUrl)
        {
            //Get value filter when push over buton Search
            Session["FlightSearchFilter"] = model.Filter.Flight;

            ViewBag.Filtro = true;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual async Task<ActionResult> Load(GridRequestViewModel gridRequest)
        {
            //Initial load data
            var filter = Session["FlightSearchFilter"] != null ? (string)Session["FlightSearchFilter"] : string.Empty;
            var count = 0;

            Filtro filtro = new Filtro { Flight = filter };

            var flightList = await flightService.GetFlights(filtro.Flight);

            var displayRecords = flightList.Count;
            var totalRecords = 10;

            //var dateFormat = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en" ? "MM/dd/yyyy" : "dd/MM/yyyy";
            //System.Globalization.DateTimeFormatInfo dtfi = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            //dtfi.DateSeparator = "/";

            //Create JSON value for pass the view
            return Json(new
            {
                iTotalDisplayRecords = displayRecords,
                iTotalRecords = totalRecords,
                sEcho = gridRequest.GridCustomData,
                aaData = flightList.Select(d => new { d.Id, d.Airline,
                    d.SourceAirportID,
                    d.SourceAirportName,
                    d.DestinationAirportID,
                    d.DestinationAirportName,
                    d.Distance,
                    d.FuelNeeded,
                    d.Stops,
                    d.Active })
            });
        }

        public async Task<ActionResult> Details(int id)
        {
            var flight = await this.flightService.GetFlight(id);

            var viewModel = Mapper.Map<Flight, DetailsFlightViewModel>(flight);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            Flight flight = new Flight();
            var viewModel = Mapper.Map<Flight, CreateFlightViewModel>(flight);

            ViewBag.AiportList = this.aiportList;

            ViewBag.IsNew = true;

            return View(viewModel);
        }

        [HttpPost]
        public async virtual Task<ActionResult> Create(CreateFlightViewModel flightModel)
        {
            if (ModelState.IsValid)
            {
                Flight flight = new Flight();

                var sourceAiport = this.aiportList.FirstOrDefault(x => x.Code == flightModel.SourceAirportID);
                var destinationAiport =this. aiportList.FirstOrDefault(x => x.Code == flightModel.DestinationAirportID);

                Mapper.Map<CreateFlightViewModel, Flight>(flightModel, flight);
                Mapper.Map<AiportViewModel, Aiport>(sourceAiport, flight.Source);
                Mapper.Map<AiportViewModel, Aiport>(destinationAiport, flight.Destination);

                flight.LastModifiedUser = User.Identity.Name;
                flight.LastModifiedDate = DateTime.Now;

                flight.Id = await flightService.PostFlight(flight);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.IsNew = true;
                ViewBag.AiportList = this.aiportList;

                return View(flightModel);
            }
        }

        public async virtual Task<ActionResult> Edit(int id)
        {
            var flight = await this.flightService.GetFlight(id);
            var viewModel = Mapper.Map<Flight, EditFlightViewModel>(flight);

            ViewBag.AiportList = this.aiportList;

            ViewBag.IsNew = false;

            return View(viewModel);
        }

        [HttpPost]
        public async virtual Task<ActionResult> Edit(EditFlightViewModel flightModel)
        {
            if (ModelState.IsValid)
            {
                var flight = await this.flightService.GetFlight(flightModel.Id);
                var sourceAiport = this.aiportList.FirstOrDefault(x => x.Code == flightModel.SourceAirportID);
                var destinationAiport = this.aiportList.FirstOrDefault(x => x.Code == flightModel.DestinationAirportID);

                Mapper.Map<EditFlightViewModel, Flight>(flightModel, flight);
                Mapper.Map<AiportViewModel, Aiport>(sourceAiport, flight.Source);
                Mapper.Map<AiportViewModel, Aiport>(destinationAiport, flight.Destination);

                flight.LastModifiedUser = User.Identity.Name;
                flight.LastModifiedDate = DateTime.Now;

                await flightService.PutFlight(flight);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AiportList = this.aiportList;

                return View(flightModel);
            }
        }

        [HttpPost]
        public async virtual Task<ActionResult> Delete(int id)
        {
            try
            {
                await flightService.DeleteFlight(id);

            }
            catch (Exception ex)
            {

                Response.StatusCode = 500;
                return Content(ex.Message);
            }
            return Content(string.Empty);
        }
    }
}
