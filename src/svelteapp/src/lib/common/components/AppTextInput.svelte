<script lang="ts">

  let { className = "", value = $bindable(), ...restProps }: {
    className?: string;
    value: string;
    [key: string]: unknown;
  } = $props();
</script>

<input {...restProps}
       bind:value
       class="input text-center p-1 variant-ghost-surface placeholder:text-surface-600 {className}"
       type="text"
/>
