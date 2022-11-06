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

global using Adnc.Shared.Application.Contracts.Attributes;
global using Adnc.Shared.Application.Contracts.ResultModels;
global using Adnc.Shared.Application.Contracts.Vos;
global using Adnc.Shared.Application.BloomFilter;
global using Adnc.Shared.Application.Channels;
global using Adnc.Shared.Application.Services;
global using Adnc.Shared.Repository.MongoEntities;
global using System.Net;
global using Adnc.Shared.Application.Contracts.Dtos;
global using System.Linq.Expressions;
global using Adnc.Infra.Repository.Entities;
global using Adnc.Share.Model;
global using Adnc.Shared.Repository.EfEntities;
global using Adnc.Shared.Repository.EfEntities.Config;
global using Adnc.Shared.WebApi.Controller;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.DependencyInjection;
global using Adnc.Infra.Caching;
global using Adnc.Infra.Caching.Configurations;
global using Adnc.Infra.Repository.IRepositories.Models;
global using AutoMapper;
global using Adnc.Maint.Application.Contracts.Dtos;
global using Adnc.Infra.Repository.IRepositories;
global using Adnc.Maint.Application.Contracts.Services;
global using Adnc.Maint.Application.Services.Caching;
global using Adnc.Maint.Entities;
global using Adnc.Infra.EventBus.RabbitMq;
global using Adnc.Shared.Consts.Mq;
global using Newtonsoft.Json;
global using Adnc.Shared.Application.Registrar;
global using Adnc.Shared.Rpc;
global using Adnc.Shared.Rpc.Rest.Services;
global using Adnc.Shared.WebApi.Registrar;
global using Adnc.Shared.Consts.Caching.Maint;
global using Adnc.Shared.Consts.Entity.Maint;
global using Adnc.Shared.Application.Contracts.Dtos.Outputs;
global using Adnc.Share.WebApi.Application.Caching;
global using Adnc.Shared.Application.Contracts.Dtos.Searchs;
global using Adnc.Infra.Core.System.Extensions.Collection;








