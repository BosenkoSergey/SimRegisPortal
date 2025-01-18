namespace Common.Domain.Data
{
    public interface IEditableEntity
    {
        DateTime DateModified { get; set; }
        int? EditorId { get; set; }

        void SetEditor(int? editorId)
        {
            DateModified = DateTime.Now;
            EditorId = editorId;
        }
    }
}
