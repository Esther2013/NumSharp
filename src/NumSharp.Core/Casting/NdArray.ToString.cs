﻿using System;
using NumSharp;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace NumSharp
{
    public partial class NDArray
    {
        public override string ToString()
        {
            return ToString(flat:false);
        }

        public string ToString(bool flat)
        {
            var s = new StringBuilder();
            if (shape.Length == 0)
            {
                s.Append($"{Storage.GetData().GetValue(0)}");
            }
            else
            {
                s.Append("array(");
                PrettyPrint(s, flat);
                s.Append(")");
            }
            return s.ToString();
        }

        private void PrettyPrint(StringBuilder s, bool flat = false)
        {
            if (shape.Length == 0)
            {
                s.Append($"{Storage.GetData().GetValue(0)}");
                return;
            }
            if (shape.Length == 1)
            {
                s.Append("[");
                s.Append(string.Join(", ", this.Array.OfType<object>().Select(x => x == null ? "null" : x.ToString())));
                s.Append("]");
                return;
            }
            var size = shape[0];
            s.Append("[");
            for (int i = 0; i < size; i++)
            {
                var n_minus_one_dim_slice = this[Slice.Index(i)];
                n_minus_one_dim_slice.PrettyPrint(s, flat);
                if (i < size - 1)
                {
                    s.Append(", ");
                    if (!flat)
                        s.AppendLine();
                }
            }
            s.Append("]");
        }
    }
    
}

