namespace SimRegisPortal.Domain.Data.Entities.Common
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
