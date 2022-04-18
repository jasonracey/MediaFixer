namespace MediaFixerLib.Workflow
{
    public interface IWorkflowRunner
    {
        void Run(IWorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus);
    }
}