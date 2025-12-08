export default function Filters({filter, setFilter}) {
    return(
         <div className='flex flex-col gap-5'>
                    <input className='p-2 rounded bg-gray-100 border
                     border-gray-300 focus:outline-none focus:ring-2
                     focus:ring-teal-300' placeholder='Search'
                     onChange={(e) => setFilter({...filter, search: e.target.value})} />
                    <select value={filter.sortOrder} className='p-2 rounded bg-gray-100 border border-gray-300 
                    focus:outline-none focus:ring-2 focus:ring-teal-300'
                    onChange={(e) => {
    console.log('Selected sortOrder:', e.target.value);
    setFilter({ ...filter, sortOrder: e.target.value });
  }} >
                      <option value='desc'>Newer first</option>
                      <option value='asc'>Older first</option>
                    </select>
                  </div>
    )
}