<script lang="ts">

  let { focus, class: _class = "", value = $bindable(), ...restProps }: {
    focus?: boolean;
    "class"?: string;
    value: string;
    [key: string]: unknown;
  } = $props();

  let input: HTMLInputElement;

  $effect(() => {
    if (focus) {
      input.focus();
    }
  });
</script>

<input {...restProps}
       bind:this={input}
       bind:value
       class="input text-center p-1 variant-ghost-surface placeholder:text-surface-600 {_class}"
       type="text"
/>
