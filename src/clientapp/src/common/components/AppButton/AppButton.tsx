import React from "react";
import "./AppButton.scss";

interface AppButtonProps extends React.HTMLAttributes<HTMLDivElement> {
  label?: string
}

const AppButton = (props: AppButtonProps) => {
  let className = props.className + " app-button";

  if (props.label !== null)
    return <label className={className}>{props.label}{props.children}</label>;

  return <div className={className}>{props.children}</div>;
}

export default AppButton;
