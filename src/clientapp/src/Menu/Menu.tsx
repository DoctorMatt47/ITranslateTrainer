import React from "react";
import {Link} from "react-router-dom";
import AppButton from "common/components/AppButton/AppButton";
import Display from "common/components/Display/Display";

const Menu = () => {
  return (
    <>
      <AppButton className="mb-4">
        <Link to="/translations">
          <Display size={4} text="Translations"/>
        </Link>
      </AppButton>
      <AppButton className="mb-4">
        <Link to="/test">
          <Display size={4} text="Test"/>
        </Link>
      </AppButton>
      <AppButton>
        <Link to="/statistics">
          <Display size={4} text="Statistics"/>
        </Link>
      </AppButton>
    </>
  );
};

export default Menu;
