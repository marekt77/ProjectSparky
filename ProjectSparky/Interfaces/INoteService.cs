using ProjectSparky.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSparky.Interfaces
{
    public interface INoteService
    {
        Task<Note> GetNote(string NoteGUID);
        Task<List<Note>> GetNotes();
        void AddNote(Note addNote);
        void UpdateNote(Note modNote);
        void DeleteNote(Note delNote);
    }
}
