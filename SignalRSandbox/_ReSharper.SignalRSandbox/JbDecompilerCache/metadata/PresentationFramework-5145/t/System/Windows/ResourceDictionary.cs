// Type: System.Windows.ResourceDictionary
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\WPF\PresentationFramework.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime;
using System.Windows.Markup;

namespace System.Windows
{
    [Localizability(LocalizationCategory.Ignore)]
    [UsableDuringInitialization(true)]
    [Ambient]
    public class ResourceDictionary : IDictionary, ICollection, IEnumerable, ISupportInitialize, IUriContext, INameScope
    {
        public Collection<ResourceDictionary> MergedDictionaries { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Uri Source { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DeferrableContent DeferrableContent { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        #region IDictionary Members

        public void Add(object key, object value);
        public void Clear();
        public bool Contains(object key);
        public IDictionaryEnumerator GetEnumerator();
        public void Remove(object key);
        void ICollection.CopyTo(Array array, int arrayIndex);
        IEnumerator IEnumerable.GetEnumerator();
        public bool IsFixedSize { get; }

        public bool IsReadOnly { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; internal set; }

        public object this[object key] { get; set; }

        public ICollection Keys { get; }
        public ICollection Values { get; }
        public int Count { get; }
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }

        #endregion

        #region INameScope Members

        public void RegisterName(string name, object scopedElement);
        public void UnregisterName(string name);
        public object FindName(string name);

        #endregion

        #region ISupportInitialize Members

        public void BeginInit();
        public void EndInit();

        #endregion

        #region IUriContext Members

        Uri IUriContext.BaseUri { get; set; }

        #endregion

        public void CopyTo(DictionaryEntry[] array, int arrayIndex);
    }
}
