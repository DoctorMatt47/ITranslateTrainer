import React from "react";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AppShell from "./common/components/AppShell/AppShell";
import Menu from "./Menu/Menu";
import NotFound from "./NotFound/NotFound";
import Translations from "./Translations/Translations";
import TestSettings from "./TestSettings/TestSettings";
import DayResults from "./Statistics/DayResults/DayResults";
import Statistics from "./Statistics/Statistics";
import TestResults from "./Statistics/TestResults/TestResults";
import TextResults from "./Statistics/TextResults/TextResults";

const AppRoutes = () => {
  return (
    <BrowserRouter>
      <AppShell>
        <Routes>
          <Route path="/" element={<Navigate to="/menu" replace/>}/>
          <Route path="/menu" element={<Menu/>}/>
          <Route path="/translations" element={<Translations/>}/>
          <Route path="/test-settings" element={<TestSettings/>}/>
          <Route path="/statistics" element={<Statistics/>}/>
          <Route path="/day-results" element={<DayResults/>}/>
          <Route path="/translation-results" element={<TextResults/>}/>
          <Route path="/test-results" element={<TestResults/>}/>
          <Route path="*" element={<NotFound/>}/>
        </Routes>
      </AppShell>
    </BrowserRouter>
  );
};

export default AppRoutes;
