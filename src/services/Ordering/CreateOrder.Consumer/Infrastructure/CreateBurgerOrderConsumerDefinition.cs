﻿using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using TooBigToFailBurgerShop.Ordering.CreateOrder.Consumer;

namespace TooBigToFailBurgerShop.Ordering.CreateOrder.Infrastructure
{
    public class CreateBurgerOrderConsumerDefinition : ConsumerDefinition<CreateBurgerOrderConsumer>
    {
        public CreateBurgerOrderConsumerDefinition()
        {
            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateBurgerOrderConsumer> consumerConfigurator)
        {
            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 3000));

            // use the outbox to prevent duplicate events from being published
            // defer messages until transactin complete
            endpointConfigurator.UseInMemoryOutbox();

        }
    }
}


