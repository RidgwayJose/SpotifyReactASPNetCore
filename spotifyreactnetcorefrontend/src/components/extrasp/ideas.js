<div>
            <table className='table table-striped table-hover mt-5 shadow-lg'>
                <thead>
                    <tr className='bg-curso text-white'>
                        <th>Nombre</th>
                        <th>Artista</th>
                        <th>Album</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user) => (
                        <tr key={user.id}>
                            <td>{user.Name}</td>
                            <td>{user.Artists}</td>
                            <td>{user.Album}</td>
                            <td><img src={user.ImageUrl} /></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>