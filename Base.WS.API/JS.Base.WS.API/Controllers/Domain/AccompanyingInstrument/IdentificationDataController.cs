﻿using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Domain
{
    [RoutePrefix("api/identificationData")]
    [Authorize]
    public class IdentificationDataController : GenericApiController<IdentificationData>
    {

        private MyDBcontext db;
        private Response response;
        private AccompanyingInstrumentService accompanyingInstrumentService;

        public IdentificationDataController()
        {
            db = new MyDBcontext();
            response = new Response();
            accompanyingInstrumentService = new AccompanyingInstrumentService();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            //Creating Accompanying Instrument Request
            var inProcess = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InProcess).FirstOrDefault();
            var request = new AccompanyingInstrumentRequest()
            {
                StatusId = inProcess.Id,
                DocentId = Convert.ToInt64(entity["DocentId"]),
                OpeningDate = DateTime.Now,
                ClosingDate = null,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestResult = db.AccompanyingInstrumentRequests.Add(request);
            db.SaveChanges();

            entity["RequestId"] = requestResult.Id;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }


        [HttpGet]
        [Route("GetAccompInstRequest")]
        public IHttpActionResult GetAccompInstRequest()
        {
            var result = accompanyingInstrumentService.GetAccompInstRequest();

            if (result.Count() == 0)
            {
                response.Code = InternalResponseCodeError.Code312;
                response.Message = InternalResponseCodeError.Message312;

                return Ok(response);
            }
            {
                response.Data = result;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("CreateVariable")]
        public IHttpActionResult CreateVariable([FromBody] long identificationDataId)
        {
            bool result = false;

            long requestId = db.IdentificationDatas.Where(x => x.Id == identificationDataId).Select(y => y.RequestId).FirstOrDefault();

            result = accompanyingInstrumentService.CreateVariables(requestId);

            if (result)
            {
                result = true;
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("GetVariableByRequestId")]
        public IHttpActionResult GetVariableByRequestId(long requestId, string variable)
        {
            var result = new VariableDto();

            result = accompanyingInstrumentService.GetVariableByRequestId(requestId, variable);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code313;
                response.Message = InternalResponseCodeError.Message313;
            }
            else
            {
                response.Data = result;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("UpdateVariable")]
        public IHttpActionResult UpdateVariable(VariableDto request)
        {
            bool result = accompanyingInstrumentService.UpdateVariable(request);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message203;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }
    }
}