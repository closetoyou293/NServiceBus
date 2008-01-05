

using Common.Logging;
using NServiceBus.Messages;
namespace NServiceBus.Unicast.Distributor
{
    public class ReadyMessageHandler : BaseMessageHandler<ReadyMessage>
    {
        public override void Handle(ReadyMessage message)
        {
            logger.Debug("Server available: " + this.Bus.SourceOfMessageBeingHandled);

            if (message.ClearPreviousFromThisAddress)
                this.workerManager.ClearAvailabilityForWorker(this.Bus.SourceOfMessageBeingHandled);

            this.workerManager.WorkerAvailable(this.Bus.SourceOfMessageBeingHandled);

        }

        private IWorkerAvailabilityManager workerManager;
        public IWorkerAvailabilityManager WorkerManager
        {
            set { workerManager = value; }
        }


        private readonly static ILog logger = LogManager.GetLogger(typeof(ReadyMessageHandler));
    }
}
