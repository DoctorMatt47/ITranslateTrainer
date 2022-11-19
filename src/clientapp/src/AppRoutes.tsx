import React from "react";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AppShell from "./common/components/AppShell/AppShell";
import Menu from "./Menu/Menu";
import NotFound from "./NotFound/NotFound";
import Translations from "./Translations/Translations";
import Quiz from "./Quiz/Quiz";

const AppRoutes = () => {
  return (
    <BrowserRouter>
      <AppShell>
        <Routes>
          <Route path="/" element={<Navigate to="/menu" replace/>}/>
          <Route path="/menu" element={<Menu/>}/>
          <Route path="/translations" element={<Translations/>}/>
          <Route path="/quiz" element={<Quiz/>}/>
          <Route path="*" element={<NotFound/>}/>
        </Routes>
      </AppShell>
    </BrowserRouter>
  );
};

export default AppRoutes;
