using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaybeMonad
{
    abstract public class Option<T>
    {
        public abstract bool TryGetValue(out T value);
        public static Option<T> Just(T value) { return new Just<T>(value); }
        public static Option<T> None() { return new None<T>(); }

        public abstract T Value { get; }

        public bool IsNone
        {
            get
            {
                if (this is None<T>)
                {
                    return true;
                }
                return false;
            }
        }
    }

    class Just<T> : Option<T>
    {
        private readonly T _value;
        public Just(T value) { _value = value; }
        public override bool TryGetValue(out T value)
        {
            value = _value;
            return true;
        }

        public override T Value { get { return _value; } }
    }

    class None<T> : Option<T>
    {
        public None() { }
        public override bool TryGetValue(out T value)
        {
            value = default(T);
            return false;
        }

        public override T Value { get { return default(T); } }
    }

    static class Maybe
    {
        public static Option<B> Select<A, B>(this Option<A> option, Func<A, B> selector)
        {
            A value;
            return option.TryGetValue(out value) ?
                Option<B>.Just(selector(value)) :
                Option<B>.None();
        }

        public static Option<B> SelectMany<A, B>(this Option<A> option, Func<A, Option<B>> selector)
        {
            A value;
            return option.TryGetValue(out value) ?
                selector(value) :
                Option<B>.None();
        }

        public static Option<C> SelectMany<A, B, C>(this Option<A> option, Func<A, Option<B>> selector, Func<A, B, C> projector)
        {
            A val0;
            B val1;
            return (option.TryGetValue(out val0) && selector(val0).TryGetValue(out val1)) ?
                Option<C>.Just(projector(val0, val1)) :
                Option<C>.None();
        }
    }
}
