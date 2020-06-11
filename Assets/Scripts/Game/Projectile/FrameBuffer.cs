using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utils;
using System.Linq;

namespace BalsamicBits.BouncyTrash.Game.Projectile
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

            shiftedFrame.Reset();

            _buffer.Enqueue(shiftedFrame);
        }

        public void ResetFrames(int id)
        {
            for (int i = 0; i < _buffer.Count; i++)
            {
                Frame frame = _buffer[i];
                if (frame.Datas.Any(d => d.Id == id))
                {
                    frame.RemoveId(id);
                }
            }
        }

        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (Frame f in _buffer)
        //    {
        //        switch (f.Storey)
        //        {
        //            case 3:
        //                sb.Append("c");
        //                break;
        //            case 2:
        //                sb.Append("b");
        //                break;
        //            case 1:
        //                sb.Append("a");
        //                break;

        //            default:
        //                sb.Append('-');
        //                break;
        //        }
        //    }

        //    return sb.ToString();
        //}

        //public string ToColourString()
        //{
        //    HSVColor colour = new HSVColor(0, 1, 1);

        //    float IdToHue(int id)
        //    {
        //        return (float)(id % 12) / 12;
        //    }

        //    StringBuilder sb = new StringBuilder();
        //    foreach (Frame f in _buffer)
        //    {
        //        if (f.Datas.HasValue)
        //        {
        //            colour.h = IdToHue(f.Datas.Value);
        //        }

        //        switch (f.Storey)
        //        {
        //            case 3:
        //                sb.Append($"<color=#{ColorUtility.ToHtmlStringRGB(colour.ToColor())}>c</color>");
        //                break;
        //            case 2:
        //                sb.Append($"<color=#{ColorUtility.ToHtmlStringRGB(colour.ToColor())}>b</color>");
        //                break;
        //            case 1:
        //                sb.Append($"<color=#{ColorUtility.ToHtmlStringRGB(colour.ToColor())}>a</color>");
        //                break;

        //            default:
        //                sb.Append('-');
        //                break;
        //        }
        //    }

        //    return sb.ToString();
        //}
    }
}