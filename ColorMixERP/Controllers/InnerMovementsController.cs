﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Logging;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using ColorMixERP.Helpers;

namespace ColorMixERP.Controllers
{
    public class InnerMovementsController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/InnerMovements")]
        public HttpResponseMessage GetInnerMovements(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<InnerMovementCommand>(query);
                int pagesCount = 0;
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                var data = new InnerMovementBL().GetInnerMovementDtos(cmd, userId, ref pagesCount);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                result.Headers.Add(Consts.PAGES_COUNT, pagesCount.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/InnerMovements/Statistical")]
        public HttpResponseMessage GetInnerMovementsStats(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<InnerMovementCommand>(query);
                int pagesCount = 0;
                var data = new InnerMovementBL().GetInnerMovementDtosStats(cmd, ref pagesCount);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                result.Headers.Add(Consts.PAGES_COUNT, pagesCount.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/InnerMovements/Group")]
        public HttpResponseMessage GetInnerMovements(int  groupId, DateTime createdDate)
        {
            try
            {
                var data = new InnerMovementBL().GetInnerMovementDtoByGroup(groupId, createdDate);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/InnerMovements/{id}")]
        public HttpResponseMessage GetInnerMovementDto(int id)
        {
            try
            {
                var data = new InnerMovementBL().GetInnerMovementDto(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/InnerMovements")]
        public HttpResponseMessage Add(InnerMovementDTO dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Add(dto, userId);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/InnerMovements/Range")]
        public HttpResponseMessage Add(InnerMovementDTO[] dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Add(dto.ToList(), userId);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/InnerMovements")]
        public HttpResponseMessage Update(InnerMovementDTO dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Update(dto, userId);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("api/InnerMovements/Range")]
        public HttpResponseMessage Update(InnerMovementDTO[] dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Update(dto, userId);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("api/InnerMovements/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                new InnerMovementBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
