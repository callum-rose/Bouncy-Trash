using System.Text;
using Utils;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    public class FrameBuffer
    {
        private CircularBuffer<Frame> _buffer;

        public Frame this[int index]
        {
            get => _buffer[index];
            set => _buffer[index] = value;
        }

        public int Count => _buffer.Count;

        public FrameBuffer(int capacity)
        {
            _buffer = new CircularBuffer<Frame>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                _buffer.Enqueue(new Frame());
            }
        }

        public void Shift()
        {
            Frame shiftedFrame = _buffer.Dequeue();

            _buffer.Enqueue(default);
        }

        public void ResetFrames(int id)
        {
            for (int i = 0; i < _buffer.Count; i++)
            {
                Frame frame = _buffer[i];
                if (frame.Id == id)
                {
                    _buffer[i] = default;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _buffer.Count; i++)
            {
                var f = _buffer[i];
                switch (f.Position)
                {
                    case 3:
                        sb.Append('c');
                        break;
                    case 2:
                        sb.Append('b');
                        break;
                    case 1:
                        sb.Append('a');
                        break;
                    default:
                        sb.Append('~');
                        break;
                }
            }

            return sb.ToString();
        }
    }
}