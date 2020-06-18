﻿using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
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
    [RoutePrefix("api/educativeCenter")]
    [Authorize]
    public class EducativeCenterController : GenericApiController<EducativeCenter>
    {

        private MyDBcontext db;
        private Response response;


        public EducativeCenterController()
        {
            db = new MyDBcontext();
            response = new Response();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            string inputShortName = entity["ShortName"];
            var centerShortName = db.EducativeCenters.Where(x => x.ShortName == inputShortName && x.IsActive == true).FirstOrDefault();

            if (centerShortName != null)
            {
                response.Code = InternalResponseCodeError.Code309;
                response.Message = InternalResponseCodeError.Message309;

                return Ok(response);
            }

            string inputName = entity["Name"];
            var centerName = db.EducativeCenters.Where(x => x.Name == inputName && x.IsActive == true).FirstOrDefault();

            if (centerName != null)
            {
                response.Code = InternalResponseCodeError.Code310;
                response.Message = InternalResponseCodeError.Message310;

                return Ok(response);
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }


        public override IHttpActionResult Update(dynamic entity)
        {

            string inputShortName = entity["ShortName"];
            int idInput = Convert.ToInt32(entity["Id"]);
            var centerShortName = db.EducativeCenters.Where(x => x.ShortName == inputShortName && x.IsActive == true).FirstOrDefault();

            if (centerShortName != null)
            {
                if (idInput != centerShortName.Id)
                {
                    response.Code = InternalResponseCodeError.Code309;
                    response.Message = InternalResponseCodeError.Message309;

                    return Ok(response);
                }
            }

            string inputName = entity["Name"];
            var centerName = db.EducativeCenters.Where(x => x.Name == inputName && x.IsActive == true).FirstOrDefault();

            if (centerName != null)
            {
                if (idInput != centerName.Id)
                {
                    response.Code = InternalResponseCodeError.Code310;
                    response.Message = InternalResponseCodeError.Message310;

                    return Ok(response);
                }
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Update(input);
        }
    }
}