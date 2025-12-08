import { useEffect, useState } from 'react'

export default function CreateNoteForm({onCreate}) {
  const [note, setNote] = useState(null);

  const onSubmit = (e) => {
    e.preventDefault();
    setNote(null);
    onCreate(note);
  }

  return (
    <form onSubmit={onSubmit} className='flex flex-col gap-3'>
            <h3 className='font-bold text-xl'>Creating a note</h3>

            <input 
              placeholder="Title" 
              value={note?.title ?? ''}
              onChange={(e) => setNote({...note, title: e.target.value})}
              className="p-2 rounded bg-gray-100 border border-gray-300 focus:outline-none focus:ring-2 focus:ring-teal-300"
            />
            <textarea 
              placeholder="Description" 
              value={note?.description ?? ''}
              onChange={(e) => setNote({...note, description: e.target.value})}
              className="p-2 rounded bg-gray-100 border border-gray-300 focus:outline-none focus:ring-2 focus:ring-teal-300"
            ></textarea>
            <button
            type="submit"
              className="bg-teal-400 hover:bg-teal-500 text-white p-2 rounded"
            >
              Create
            </button>

          </form>

  )
}