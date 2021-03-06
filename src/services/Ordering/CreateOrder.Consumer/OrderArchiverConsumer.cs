﻿using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TooBigToFailBurgerShop.Ordering.Domain;
using TooBigToFailBurgerShop.Ordering.Domain.Events;

namespace TooBigToFailBurgerShop.Ordering.CreateOrder.Consumer
{
    public class OrderArchiverConsumer : IConsumer<OrderCreated>
    {
        private readonly ILogger<CreateBurgerOrderConsumer> _logger;
        private readonly IOrderArchiveItemRepository _orderArchiveItemRepository;

        public OrderArchiverConsumer(IOrderArchiveItemRepository ordersRepository, ILogger<CreateBurgerOrderConsumer> logger)
        {
            _logger = logger;
            _orderArchiveItemRepository = ordersRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            _logger.LogInformation("OrderArchiverConsumer OrderId: {id}", context.Message.AggregateId);

            var @event = context.Message;

            await _orderArchiveItemRepository.CreateAsync(@event.AggregateId, @event.Timestamp);
        }
    }

}
