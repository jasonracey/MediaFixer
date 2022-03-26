namespace MediaFixerLib.Workflow
{
    public interface IWorkflowRunner
    {
        void Run(WorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus);
    }
}