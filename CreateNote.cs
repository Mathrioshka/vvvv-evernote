using Evernote.EDAM.Type;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Nodes.Evernote
{
    [PluginInfo(Name = "CreateNote", Category = "Evernote", Help = "", Tags = "")]
    public class CreateNote : IPluginEvaluate
    {
        [Input("Note")]
        private ISpread<Note> FNoteIn;

        [Input("Evernote", IsSingle = true)]
        private ISpread<Evernote> FEvernoteIn;

        [Input("Create", IsBang = true, IsSingle = true)]
        private ISpread<bool> FCreateIn;
        
        public void Evaluate(int SpreadMax)
        {
            if(FCreateIn[0])
            {
                for (var i = 0; i < SpreadMax; i++) 
                {
                    FEvernoteIn[0].NoteStore.createNote(FEvernoteIn[0].Token, FNoteIn[i]);
                }
            }
        }
    }
}
