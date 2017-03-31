using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders;
using Cmas.DataLayers.CouchDb.CallOffOrders.Queries;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Cmas.BusinessLayers.CallOffOrders.Criteria;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.DataLayers.CouchDb.CallOffOrders.Commands;

namespace ConsoleTests
{
    class Program
    {
        private static IMapper _mapper;

        static void Main(string[] args)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                _mapper = config.CreateMapper();
                 
                
                //AllEntitiesTest().Wait();
                string id = CreateCallOffOrderCommand().Result;
               
                //UpdateCallOffOrderCommand("26270cfa2422b2c4ebf158285e06440e").Wait();
            
              //var res = FindByIdQueryTest(id).Result;

               //var res2 = FindByContractIdQueryTest("26270cfa2422b2c4ebf158285e050f11").Result;

               //DeleteCallOffOrderCommand(id).Wait();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.ReadKey();
        }

        static async Task<bool> FindByIdQueryTest(string _id)
        {
            FindByIdQuery findByIdQuery = new FindByIdQuery(_mapper);
            FindById criterion = new FindById(_id);
            CallOffOrder result = null;

            try
            {
                result = await findByIdQuery.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Id);

            return true;
        }

        static async Task<bool> FindByContractIdQueryTest(string contractId)
        {
            var query = new FindByContractIdQuery(_mapper);
            var criterion = new FindByContractId();
            criterion.ContractId = contractId;

            IEnumerable<CallOffOrder> result = null;

            try
            {
                result = await query.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Count());

            return true;
        }

        static async Task<string> CreateCallOffOrderCommand()
        {
            var commandContext = new CreateCallOffOrderCommandContext();

            var order = new CallOffOrder();

            order.ContractId = "26270cfa2422b2c4ebf158285e01c610";
            
            commandContext.Form = order;

            var command = new CreateCallOffOrderCommand(_mapper);
              
            var result = await command.Execute(commandContext);

            return result.Id;
        }

        static async Task<bool> DeleteCallOffOrderCommand(string id)
        {
            var commandContext = new DeleteCallOffOrderCommandContext();
            var command = new DeleteCallOffOrderCommand();

            commandContext.Id = id;

            var result = await command.Execute(commandContext);

            return true;
        }

        static async Task<bool> UpdateCallOffOrderCommand(string id)
        {
            var commandContext = new UpdateCallOffOrderCommandContext();
            var command = new UpdateCallOffOrderCommand(_mapper);

            var order = new CallOffOrder();

            order.Id = id;

            order.Name = "Заказег";
            order.Number = "123/sdhgf";
            order.ContractId = "26270cfa2422b2c4ebf158285e01c610";
            order.Assignee = "ФИОООО";

            {
                var rate = new Rate();
                rate.Id = 1;
                rate.Name = "Услуга";
 
                order.Rates.Add(rate);
            }

            {
                var rate = new Rate();
                rate.Id = 2;
                rate.Name = "Выходные";
                rate.Amount = 1000;
                rate.Currency = "RUR";
                rate.UnitName = "дн.";
                rate.IsRate = true;

                order.Rates.Add(rate);
            }

            commandContext.Form = order;

           


            var result = await command.Execute(commandContext);

            return true;
        }

        static async Task<bool> AllEntitiesTest()
        {
            AllEntitiesQuery query = new AllEntitiesQuery(_mapper);
            AllEntities criterion = new AllEntities();
            IEnumerable<CallOffOrder> result = null;

            try
            {
                result = await query.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Count());

            return true;
        }
    }
}