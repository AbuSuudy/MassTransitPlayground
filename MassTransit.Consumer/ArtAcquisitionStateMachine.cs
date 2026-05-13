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

        //States
        public State Submitted { get; private set; } = null!;
        public State Acquired { get; private set; } = null!;
        public State Rejected { get; private set; } = null!;

        public ArtAcquisitionStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => BidSubmitted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => BidAccepted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => BidRejected, x => x.CorrelateById(m => m.Message.Id));

            Initially(
                When(BidSubmitted)
                .TransitionTo(Submitted));

            During(Submitted,
                 When(BidRejected)
                 .TransitionTo(Rejected)
                 .Finalize()
             );

            During(Submitted,
                When(BidAccepted)
                .TransitionTo(Acquired)
                .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
