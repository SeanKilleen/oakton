using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Oakton;

namespace MultipleCommands
{
    class Program
    {
        #region sample_MultipleCommands.Program.Main
        static int Main(string[] args)
        {
            var executor = CommandExecutor.For(_ =>
            {
                // Find and apply all command classes discovered
                // in this assembly
                _.RegisterCommands(typeof(Program).GetTypeInfo().Assembly);
            });

            return executor.Execute(args);
        }
        #endregion

            /*
        #region sample_MultipleCommands.Program.Main.Async
        static Task<int> Main(string[] args)
        {
            var executor = CommandExecutor.For(_ =>
            {
                // Find and apply all command classes discovered
                // in this assembly
                _.RegisterCommands(typeof(Program).GetTypeInfo().Assembly);
            });

            return executor.ExecuteAsync(args);
        }
        #endregion
        */
    }

    #region sample_git_commands
    [Description("Switch branches or restore working tree files")]
    public class CheckoutCommand : OaktonAsyncCommand<CheckoutInput>
    {
        public override async Task<bool> Execute(CheckoutInput input)
        {
            await Task.CompletedTask;
            return true;
        }
    }
    
    [Description("Remove untracked files from the working tree")]
    public class CleanCommand : OaktonCommand<CleanInput>
    {
        public override bool Execute(CleanInput input)
        {
            return true;
        }
    }
    #endregion

    #region sample_CheckoutInput
    public class CheckoutInput
    {
        [FlagAlias("create-branch",'b')]
        public string CreateBranchFlag { get; set; }
        
        public bool DetachFlag { get; set; }
        
        public bool ForceFlag { get; set; }
    }
    #endregion


    #region sample_CleanInput
    public class CleanInput
    {
        [Description("Do it now!")]
        public bool ForceFlag { get; set; }
        
        [FlagAlias('d')]
        [Description("Remove untracked directories in addition to untracked files")]
        public bool RemoveUntrackedDirectoriesFlag { get; set; }
        
        [FlagAlias('x')]
        [Description("Remove only files ignored by Git")]
        public bool DoNoUseStandardIgnoreRulesFlag { get; set; }
    }
    #endregion


}
