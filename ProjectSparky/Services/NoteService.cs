using Microsoft.EntityFrameworkCore;
using ProjectSparky.Interfaces;
using ProjectSparky.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSparky.Services
{
    public class NoteService : INoteService
    {
        public async Task<Note> GetNote(string NoteGUID)
        {
            Note rNote = null;

            using (var db = new ProjectSparyContext())
            {
                rNote = await db.Notes.FirstOrDefaultAsync(x => x.NoteGUID == NoteGUID);
            }

            return rNote;
        }

        public async Task<List<Note>> GetNotes()
        {
            List<Note> rNotes = null;

            using (var db = new ProjectSparyContext())
            {
                rNotes = await db.Notes.ToListAsync();
            }

            return rNotes;
        }

        public async void AddNote(Note addNote)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Notes.Add(addNote);
                await db.SaveChangesAsync();
            }
        }

        public async void UpdateNote(Note modNote)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Notes.Update(modNote);
                await db.SaveChangesAsync();
            }
        }

        public async void DeleteNote(Note delNote)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Notes.Remove(delNote);
                await db.SaveChangesAsync();
            }
        }
    }
}
