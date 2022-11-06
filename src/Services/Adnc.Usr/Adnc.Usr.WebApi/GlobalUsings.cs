global using System;
global using System.Linq;
global using System.Reflection;
global using System.Threading.Tasks;
global using System.Collections.Generic;

global using NLog;
global using NLog.Web;
global using FluentValidation;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Hosting;
global using MongoDB.Driver;

global using Adnc.Infra.Core.System.Extensions.String;
global using Adnc.Infra.IdGenerater.Yitter;
global using Adnc.Infra.Core.Adnc.Interfaces;
global using Adnc.Share.WebApi.WebApi.Registrar;

global using Adnc.Infra.Caching.Core.Interceptor;
global using Adnc.Shared.Application.Contracts.Attributes;
global using Adnc.Shared.Application.Contracts.ResultModels;
global using Adnc.Shared.Application.Contracts.Vos;
global using Adnc.Shared.Consts.Caching.Usr;
global using Adnc.Usr.Entities;
global using Adnc.Usr.WebApi.Models.Dtos.Users;
global using Adnc.Usr.WebApi.Models.Vos.Users;
global using Adnc.Usr.Application.Contracts.Dtos;

global using Adnc.Infra.Helper;
global using Adnc.Infra.Repository.IRepositories;
global using Adnc.Shared.Application.BloomFilter;
global using Adnc.Shared.Application.Channels;
global using Adnc.Shared.Application.Services;
global using Adnc.Shared.Repository.MongoEntities;
global using Adnc.Usr.Application.Caching;
global using Adnc.Usr.Application.Contracts.Services;
global using System.Net;
global using Adnc.Shared.Application.Contracts.Dtos;
global using System.Linq.Expressions;
global using Adnc.Infra.Repository.Entities;
global using System.Collections.ObjectModel;
global using Adnc.Share.Model;
global using Adnc.Shared.Repository.EfEntities;
global using Adnc.Shared.Consts.Entity.Usr;
global using Adnc.Shared.Repository.EfEntities.Config;
global using Adnc.Shared.Consts.Permissions.Usr;
global using Adnc.Shared.WebApi.Authorization;
global using Adnc.Shared.WebApi.Controller;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using NSwag.Annotations;
global using Adnc.Infra.Core.Adnc.Configuration;
global using Adnc.Shared.WebApi.Authentication.JwtBearer;
global using Microsoft.Extensions.Options;
global using System.IdentityModel.Tokens.Jwt;
global using Adnc.Usr.WebApi.Authentication;
global using Adnc.Usr.WebApi.Authorization;
global using Microsoft.Extensions.DependencyInjection;
global using Adnc.Infra.Caching;
global using Adnc.Share.WebApi.Application.Caching;
global using Adnc.Infra.Caching.Configurations;
global using Adnc.Infra.Repository.IRepositories.Models;
global using AutoMapper;
global using Adnc.Shared.WebApi.Authentication;






