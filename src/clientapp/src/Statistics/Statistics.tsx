import AppButton from "../common/components/AppButton/AppButton";
import {Link} from "react-router-dom";
import Display from "../common/components/Display/Display";
import React from "react";

const Menu = () => {
  return (
    <>
      <AppButton className="mb-4">
        <Link to="/day-results">
          <Display size={4} text="Day results"/>
        </Link>
      </AppButton>
      <AppButton className="mb-4">
        <Link to="/test-results">
          <Display size={4} text="Test results"/>
        </Link>
      </AppButton>
      <AppButton>
        <Link to="/translation-results">
          <Display size={4} text="Translation results"/>
        </Link>
      </AppButton>
    </>
  );
};

export default Menu;
