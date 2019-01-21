using System;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using Thrift.Protocol;
using Thrift.Transport;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Nodes.Evernote
{
    [PluginInfo(Name = "Connect", Category = "Evernote", Help = "", Tags = "")]
	public class EvernoteConnect : IPluginEvaluate
	{
		[Input("Connect", IsBang = true, IsSingle = true)]
		private ISpread<bool> FConnectIn;

		[Input("Token", IsSingle = true)]
		private ISpread<string> FTokenIn;

		[Output("Evernote", IsSingle = true)]
		private ISpread<Evernote> FEvernoteOut;

		//Evernote Routine
        private string FEvernoteHost = "sandbox.evernote.com";
        private Uri FUserStoreUrl;
        private TTransport FUserStoreTransport;
        private TProtocol FUserStoreProtocol;
        private UserStore.Client FUserStore;

        private string FNoteStoreUrl;

        private TTransport FNoteStoreTransport;
        private TProtocol FNoteStoreProtocol;
        private NoteStore.Client FNoteStore;

        public EvernoteConnect()
        {
            FUserStoreUrl = new Uri("https://" + FEvernoteHost + "/edam/user");
            FUserStoreTransport = new THttpClient(FUserStoreUrl);
            FUserStoreProtocol = new TBinaryProtocol(FUserStoreTransport);
            FUserStore = new UserStore.Client(FUserStoreProtocol);
        }

	    private void Init()
        {
        	FNoteStoreUrl = FUserStore.getNoteStoreUrl(FTokenIn[0]);
        	FNoteStoreTransport = new THttpClient(new Uri(FNoteStoreUrl));
        	FNoteStoreProtocol = new TBinaryProtocol(FNoteStoreTransport);
        	FNoteStore = new NoteStore.Client(FNoteStoreProtocol);
        }

        public void Evaluate(int spreadMax)
        {
        	FEvernoteOut[0] = new Evernote(){Token = FTokenIn[0], UserStore = FUserStore, NoteStore = FNoteStore};
        }
		
	}

	public struct Evernote
	{
		public string Token;
		public UserStore.Client UserStore;
		public NoteStore.Client NoteStore;
	}
}