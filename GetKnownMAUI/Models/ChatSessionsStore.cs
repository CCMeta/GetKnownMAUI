using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetKnownMAUI.Models
{
    public class ChatSessionsStore : BaseStore
    {

        public ChatSessionsStore()
        {
            Task.Run(async () =>
            {
                //if (db.TableMappings.Count(i => i.TableName == "ChatSessions") < 1)
                //await db.DropTableAsync<MyContacts>();
                await db.CreateTableAsync<MyContacts>();

                //var result = await ListAsync();
            }).Wait();
        }

        public Task<List<MyContacts>> ListAsync()
        {
            //Get all notes.
            return db.Table<MyContacts>().ToListAsync();
        }

        public Task<MyContacts> GetAsync(int partner)
        {
            // Get a specific note.
            return db.Table<MyContacts>().Where(i => i.partner_id == partner).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(MyContacts item)
        {
            if (item.id != 0)
            {
                // Update an existing note.
                return db.UpdateAsync(item);
            }
            else
            {
                // Save a new note.
                return db.InsertAsync(item);
            }
        }

        public Task<int> DeleteAsync(MyContacts item)
        {
            // Delete a note.
            return db.DeleteAsync(item);
        }
    }
}
