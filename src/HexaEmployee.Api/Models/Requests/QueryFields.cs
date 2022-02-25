using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HexaEmployee.Api.Models.Requests
{
    [Serializable]
    public abstract class QueryFields : Dictionary<string, string>
    {
        protected QueryFields()
        {
        }

        protected QueryFields(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }

        protected QueryFields(
            IDictionary<string, string> dictionary,
            IEqualityComparer<string> comparer)
            : base(dictionary, comparer)
        {
        }

        protected QueryFields(IEnumerable<KeyValuePair<string, string>> collection)
            : base(collection)
        {
        }

        protected QueryFields(
            IEnumerable<KeyValuePair<string, string>> collection,
            IEqualityComparer<string> comparer)
            : base(collection, comparer)
        {
        }

        protected QueryFields(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        protected QueryFields(int capacity)
            : base(capacity)
        {
        }

        protected QueryFields(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }

        protected QueryFields(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
