using MassTransit.Contract;

namespace MassTransit.Consumer
{
    public class ArtAcquisition : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
    }

    public class ArtAcquisitionStateMachine : MassTransitStateMachine<ArtAcquisition>
    {
        //Transitions between the states
        public Event<BidSubmitted> BidSubmitted { get; private set; }
        public Event<BidAccepted> BidAccepted { get; private set; }
        public Event<BidRejected> BidRejected { get; private set; }
        public Event<CounterOffer> CounterOffer { get; private set; }
        public Event<CounterOfferAccepted> CounterOfferAccepted { get; private set; }
        public Event<CounterOfferRejected> CounterOfferRejected { get; private set; }

        //States
        public State Submitted { get; private set; } = null!;
        public State Counter { get; private set; } = null!;
        public State Rejected { get; private set; } = null!;
        public State Acquired { get; private set; } = null!;

        public ArtAcquisitionStateMachine()
        {
            Event(() => BidSubmitted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => BidAccepted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => BidRejected, x => x.CorrelateById(m => m.Message.Id));
            Event(() => CounterOffer, x => x.CorrelateById(m => m.Message.Id));
            Event(() => CounterOfferAccepted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => CounterOfferRejected, x => x.CorrelateById(m => m.Message.Id));

            InstanceState(x => x.CurrentState);

            Initially(
                When(BidSubmitted)
                .TransitionTo(Submitted)
                .Finalize());

            //During(Submitted,

            //    When(BidAccepted)
            //    .PublishAsync(x => x.Init<BidAccepted>(new
            //    {
            //        Id = x.Message.Id,
            //        Bid = x.Message.Bid
            //    }))
            //    .TransitionTo(Acquired)
            //    .Finalize(),

            //    When(BidRejected)
            //    .PublishAsync(x => x.Init<BidRejected>(new
            //    {
            //        Id = x.Message.Id,
            //        Bid = x.Message.Bid
            //    }))
            //    .TransitionTo(Rejected)
            //    .Finalize(),

            //    When(CounterOffer)
            //    .If(x => x.Message.bid >= x.Message.Offer,
            //        then => then.PublishAsync(x => x.Init<CounterOffer>(new
            //        {
            //            Id = x.Message.Id,
            //            Offer = x.Message.Offer
            //        }))
            //        .TransitionTo(Counter)
            //        ).PublishAsync(x => x.Init<CounterOfferRejected>(new
            //        {
            //            Id = x.Message.Id,
            //            Offer = x.Message.Offer
            //        }))
            //        .TransitionTo(Rejected).Finalize()
            //);

            //During(Counter,
            //    When(CounterOfferAccepted)
            //        .TransitionTo(Acquired)
            //        .Finalize(),

            //    When(CounterOfferRejected)
            //        .PublishAsync(x => x.Init<CounterOfferRejected>(new
            //        {
            //            Id = x.Message.Id,
            //            Offer = x.Message.Offer
            //        }))
            //        .TransitionTo(Rejected)
            //        .Finalize()
            //);

            SetCompletedWhenFinalized();
        }
    }
}
