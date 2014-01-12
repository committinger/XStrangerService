using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerServic.Core
{
    public class Circle
    {
        private static object _lockObj = new object();
        private static Dictionary<string, Circle> _pool;
        public static Dictionary<string, Circle> Pool { get { return _pool; } }

        public void Initialize()
        {
            _pool = new Dictionary<string, Circle>();
        }

        private string _name;
        private string[] _key;
        public string[] Key { get { return _key; } }
        public string Name { get { return _name; } }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<string> ImageUrlList { get; set; }

        public Circle(string name)
        {
            this._name = name;
            this._key = new string[] { name };
            ImageUrlList = new List<string>();
        }

        public void AddKey(string key)
        {
            lock (_lockObj)
                if (!this._key.Contains(key) && !Pool.ContainsKey(key))
                {
                    Array.Resize(ref this._key, this._key.Length + 1);
                    this._key[this._key.Length - 1] = key;
                    Pool.Add(key, this);
                }
        }

        public void RemoveKey(string key)
        {
            lock (_lockObj)
                if (!string.IsNullOrEmpty(key) && this._key.Contains(key))
                {
                    this._key[Array.FindIndex(this._key, t => string.Equals(key, t))] = string.Empty;
                    Pool.Remove(key);
                }
        }

        public static void AddCircle(Circle circle)
        {
            lock (_lockObj)
                if (circle != null)
                    if (Array.TrueForAll(circle.Key, t =>
                    {
                        return string.IsNullOrEmpty(t) || (!Pool.ContainsKey(t));
                    }))
                        Array.ForEach(circle.Key, t =>
                        {
                            if (!string.IsNullOrEmpty(t)) Pool.Add(t, circle);
                        });
        }
    }
}
