namespace FinalEngine.Editor.Services.Instructions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class InstructionsManager : IInstructionsManager
    {
        private readonly ILogger<InstructionsManager> logger;

        private readonly Stack<IInstruction> redoInstructions;

        private readonly Stack<IInstruction> undoInstructions;

        public InstructionsManager(ILogger<InstructionsManager> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.undoInstructions = new Stack<IInstruction>();
            this.redoInstructions = new Stack<IInstruction>();
        }

        public event EventHandler InstructionsModified;

        public bool CanRedo
        {
            get { return this.redoInstructions.Count > 0; }
        }

        public bool CanUndo
        {
            get { return this.undoInstructions.Count > 0; }
        }

        public void PerformInstruction(IInstruction instruction)
        {
            this.undoInstructions.Push(instruction);
            this.redoInstructions.Clear();

            this.logger.LogInformation($"Performing {instruction.GetType().Name} instruction...");

            instruction.Execute();

            this.InstructionsModified?.Invoke(this, EventArgs.Empty);
        }

        public void Redo()
        {
            if (!this.redoInstructions.TryPop(out var instruction))
            {
                return;
            }

            this.undoInstructions.Push(instruction);
            this.InstructionsModified?.Invoke(this, EventArgs.Empty);

            this.logger.LogInformation($"Redoing {instruction.GetType().Name} instruction...");

            instruction.Execute();
        }

        public void Undo()
        {
            if (!this.undoInstructions.TryPop(out var instruction))
            {
                return;
            }

            this.redoInstructions.Push(instruction);
            this.InstructionsModified?.Invoke(this, EventArgs.Empty);

            this.logger.LogInformation($"Undoing {instruction.GetType().Name} instruction...");

            instruction.UnExecute();
        }
    }
}
