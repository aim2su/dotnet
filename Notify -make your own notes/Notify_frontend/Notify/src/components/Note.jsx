import moment from 'moment';
export default function Note({title, description, createdAt}) {
    return(
    <div className="bg-teal-50 rounded-lg shadow p-4 flex flex-col mt-4">
          
      <div className="mb-2">
        <h3 className="text-md font-semibold">{title}</h3>
      </div>

      <div className="border-t border-gray-300 my-2"></div>

      <div className="mb-2">
        <p className="text-gray-700">{description}</p>
      </div>

      <div className="border-t border-gray-300 my-2"></div>

      <div className="text-sm text-gray-500">
        {moment(createdAt).format("DD/MM/YYYY hh:mm")}
      </div>
    </div>
    )    
}