using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;

namespace RenRen.Domain.Common.Extension
{
    public static class MapExtension
    {
        public static List<T> Map<S, T>(this List<S> source)
        {
            List<T> list = new List<T>();
            if (!TinyMapper.BindingExists<S, T>())
            {
                TinyMapper.Bind<S, T>();
            }

            source.ForEach(s =>
            {
                list.Add(TinyMapper.Map<T>(s));
            });

            return list;
        }

        public static T Map<S, T>(this S source)
        {
            if (!TinyMapper.BindingExists<S, T>())
            {
                TinyMapper.Bind<S, T>();
            }
            return TinyMapper.Map<T>(source);
        }
    }
}
