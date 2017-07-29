namespace HabitRPG.Client.Model
{
    using HabitRPG.Client.Extensions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChecklistTaskItem : TaskItem
    {
        protected ChecklistTaskItem() : base()
        {
            InternalChecklist = new List<Checklist>();
        }

        #region Properties

        [JsonProperty("collapseChecklist")]
        public bool CollapseChecklist { get; set; }

        [JsonProperty("checklist", NullValueHandling = NullValueHandling.Ignore)]
        protected List<Checklist> InternalChecklist { get; set; }

        [JsonIgnore]
        public IReadOnlyList<Checklist> Checklist
        {
            get
            {
                return InternalChecklist;
            }
        }

        #endregion Properties

        public async Task AddChecklistItemAsync(Checklist checklist)
        {
            if (checklist == null)
            {
                throw new ArgumentNullException("checklist");
            }

            if (Id == Guid.Empty)
            {
                await SaveAsync();
            }

            var response = await HttpClient.PostAsJsonAsync(String.Format("tasks/{0}/checklist", Id), checklist);
            CopyFrom(GetResult<ChecklistTaskItem>(response));
        }

        public async Task DeleteChecklistItemAsync(Checklist checklist)
        {
            if (checklist == null)
            {
                throw new ArgumentNullException("checklist");
            }

            if (checklist.Id == Guid.Empty)
            {
                throw new ArgumentException("checklist");
            }

            var response = await HttpClient.DeleteAsync(String.Format("tasks/{0}/checklist/{1}", Id, checklist.Id));
            CopyFrom(GetResult<ChecklistTaskItem>(response));
        }

        public async Task ScoreChecklistItemAsync(Checklist checklist)
        {
            if (checklist == null)
            {
                throw new ArgumentNullException("checklist");
            }

            if (checklist.Id == Guid.Empty)
            {
                throw new ArgumentException("checklist");
            }

            var response = await HttpClient.PostAsync(String.Format("tasks/{0}/checklist/{1}/score", Id, checklist.Id), null);
            CopyFrom(GetResult<ChecklistTaskItem>(response));
        }

        public async Task UpdateChecklistItem(Checklist checklist)
        {
            if (checklist == null)
            {
                throw new ArgumentNullException("checklist");
            }

            if (checklist.Id == Guid.Empty)
            {
                throw new ArgumentException("checklist");
            }

            var response = await HttpClient.PutAsJsonAsync(String.Format("tasks/{0}/checklist/{1}", Id, checklist.Id), checklist);
            CopyFrom(GetResult<ChecklistTaskItem>(response));
        }

        protected void UpdateCollection<T>(List<Checklist> original, List<Checklist> amend, bool deleteStaleEntries = true)
        {
            if (deleteStaleEntries)
            {
                List<Checklist> itemsToRemove = new List<Checklist>();
                foreach (var item in original.ToList())
                {
                    // If an item exists in original, but not in amend, then it should be removed from original
                    var amendObject = amend.SingleOrDefault(i => i.Id == item.Id);
                    if (amendObject == null)
                    {
                        // Cannot remove item here - will cause a concurrency issue
                        itemsToRemove.Add(item);
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    original.Remove(item);
                }
            }

            foreach (var item in amend)
            {
                var originalObject = original.SingleOrDefault(i => i.Id == item.Id);
                if (originalObject == null)
                {
                    // If it doesn't exist in original yet, add it
                    original.Add(item);
                }
                else
                {
                    // Otherwise, update the existing item
                    originalObject.CopyFrom(item);
                }
            }
        }

        protected override void CopyFrom(TaskItem item)
        {
            ChecklistTaskItem todo = (ChecklistTaskItem)item;
            base.CopyFrom(item);
            CollapseChecklist = todo.CollapseChecklist;
            UpdateCollection<Checklist>(InternalChecklist, todo.InternalChecklist);
        }
    }
}
