import React, { useEffect, useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom'

import Budgets from "./Components/Budgets/budgets"
import Expenses from "./Components/Expenses/expenses"
import Goals from "./Components/Goals/goals"
import Incomes from "./Components/Incomes/incomes"
import Layout from "./Components/Layout/layout"
import Login from "./Components/Login/login"
import Logout from "./Components/Logout/logout"

const App = () => {

  const [user, setUser] = useState({
    login: null,
    token: null
  })

  return (
    <BrowserRouter>
        <Routes>
            <Route path = "/" element = {<Layout user = {user}/>}>
                <Route index element = {<Login user = {user} setUser={setUser}/>}/>

                <Route
                  path="/budgets"
                  element={<Budgets user = {user}/>}
                />

                <Route
                  path="/expenses"
                  element={<Expenses user = {user}/>}
                />

                <Route
                  path="/goals"
                  element={<Goals user = {user}/>}
                />

                <Route
                  path="/incomes"
                  element={<Incomes user = {user}/>}
                />

                <Route
                  path="/logout"
                  element={<Logout user = {user}/>}
                />                    
            </Route>
        </Routes>
    </BrowserRouter>
  )
}

export default App;
