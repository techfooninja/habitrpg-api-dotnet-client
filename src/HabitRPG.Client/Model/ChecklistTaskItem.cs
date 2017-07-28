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
            InternalChecklist = new Dictionary<Guid, Checklist>();
        }

        #region Properties

        [JsonProperty("collapseChecklist")]
        public bool CollapseChecklist { get; set; }

        [JsonProperty("checklist")]
        protected Dictionary<Guid, Checklist> InternalChecklist { get; set; }

        [JsonIgnore]
        public IReadOnlyList<Checklist> Checklist
        {
            get
            {
                return InternalChecklist.Values.ToList();
            }
        }

        #endregion Properties

        public async Task AddChecklistItemAsync(Checklist checklist)
        {
            if (checklist == null)
            {
                throw new ArgumentNullException("checklist");
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

            var response = await HttpClient.PostAsJsonAsync(String.Format("tasks/{0}/checklist/{1}", Id, checklist.Id), null);
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

        protected override void CopyFrom(TaskItem item)
        {
            ChecklistTaskItem todo = (ChecklistTaskItem)item;
            base.CopyFrom(item);
            CollapseChecklist = todo.CollapseChecklist;
            UpdateCollection<Checklist>(InternalChecklist, todo.InternalChecklist);
        }
    }
}
