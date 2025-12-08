import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Button, HStack, Input, Textarea } from "@chakra-ui/react"

function App() {

  return (
    <>
    <section className='p-8 flex flex-row justify-start items-start gap-12'>
      <div className='flex flex-col w-1/3 gap-10'>
        <form className='w-full flex flex-col gap-3'>
          <h3 className='font-bold text-xl'>Creating a note</h3>
          <Input placeholder="Title" />
          <Textarea placeholder="Description"/>
          <Button colorScheme='teal'>Create</Button>
        </form>
      </div>
    </section>
    </>
  )
}

export default App
