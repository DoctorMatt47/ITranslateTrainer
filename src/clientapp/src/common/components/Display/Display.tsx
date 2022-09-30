import React from "react";

interface DisplayProps extends React.HTMLAttributes<HTMLDivElement> {
  size: number;
  text: string;
}

const Display = (props: DisplayProps) =>
  <div className={props.className + " display-" + props.size}>
    {props.text}
  </div>

export default Display;
