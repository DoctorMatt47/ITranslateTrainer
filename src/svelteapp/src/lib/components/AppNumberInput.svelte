<script lang="ts">
  export let value: number;
  export let className = "";

</script>

<input {...$$restProps}
       bind:value
       class="input text-center p-1 variant-ghost-surface placeholder:text-surface-600 {className}"
       type="number"
/>

<style>
    input::-webkit-outer-spin-button,
    input::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
    }

    input[type=number] {
        -moz-appearance: textfield; /* Firefox */
        appearance: textfield; /* Safari and Chrome */
    }
</style>
