<script lang="ts">
  export let value: string;
  export let className = "";

</script>

<input {...$$restProps}
       bind:value
       class="input text-center p-1 variant-ghost-surface placeholder:text-surface-600 {className}"
       type="text"
/>
