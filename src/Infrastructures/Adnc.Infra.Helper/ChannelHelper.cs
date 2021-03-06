using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Adnc.Infra.Helper
{
    public class ChannelHelper<TModel>
    {
        private static readonly Lazy<ChannelHelper<TModel>> lazy = new Lazy<ChannelHelper<TModel>>(() => new ChannelHelper<TModel>());

        private readonly ChannelWriter<TModel> _writer;
        private readonly ChannelReader<TModel> _reader;

        static ChannelHelper()
        {
        }

        private ChannelHelper()
        {
            var channelOptions = new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.DropOldest
            };
            var channel = Channel.CreateBounded<TModel>(channelOptions);
            _writer = channel.Writer;
            _reader = channel.Reader;
        }

        public static ChannelHelper<TModel> Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public ChannelWriter<TModel> Writer => _writer;

        public ChannelReader<TModel> Reader => _reader;
    }
}
